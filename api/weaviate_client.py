# weaviate_client.py
import weaviate
from weaviate.classes.config import Property, DataType, Configure
from weaviate.classes.query import MetadataQuery
from typing import List, Dict, Any
import time

class WeaviateClient:
    def __init__(self):
        for i in range(10):
            try:
                self.client = weaviate.connect_to_local(
                    host="localhost",
                    port=8089,
                    grpc_port=50051
                )
                break
            except Exception as e:
                if i == 9:
                    raise e
                time.sleep(5)
        
    def create_schema(self):
        try:
            self.client.collections.delete("Knowledge")
        except:
            pass
        
        self.client.collections.create(
            name="Knowledge",
            vectorizer_config=Configure.Vectorizer.text2vec_transformers(
                vectorize_collection_name=False
            ),
            properties=[
                Property(name="content", data_type=DataType.TEXT),
                Property(name="media_type", data_type=DataType.TEXT),
                Property(name="source", data_type=DataType.TEXT),
            ]
        )
    
    def index_document(self, content: str, media_type: str, source: str, meta: Dict = None):
        collection = self.client.collections.get("Knowledge")
        
        uuid = collection.data.insert({
            "content": content,
            "media_type": media_type,
            "source": source,
        })
        return uuid
    
    def search(self, query: str, limit: int = 10) -> List[Dict[str, Any]]:
        collection = self.client.collections.get("Knowledge")
        
        # Явно запрашиваем distance
        response = collection.query.near_text(
            query=query,
            limit=limit,
            return_metadata=MetadataQuery(distance=True)  # <- добавлено
        )
        
        results = []
        for obj in response.objects:
            # теперь distance доступен
            distance = obj.metadata.distance
            relevance = 1 - distance if distance else 0.0
            
            results.append({
                "id": str(obj.uuid),
                "content": obj.properties["content"],
                "media_type": obj.properties["media_type"],
                "relevance": relevance,
                "source": obj.properties["source"]
            })
        return results
    
    def close(self):
        self.client.close()