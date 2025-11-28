# api.py
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException, UploadFile, File
from models import SearchRequest, SearchResult, IndexRequest
from weaviate_client import WeaviateClient
from image_processor import ImageProcessor
from typing import List
import base64

@asynccontextmanager
async def lifespan(app: FastAPI):
    global weaviate_client, image_processor
    weaviate_client = WeaviateClient()
    weaviate_client.create_schema()
    image_processor = ImageProcessor()
    yield
    weaviate_client.close()

app = FastAPI(lifespan=lifespan)

@app.post("/index")
async def index_document(request: IndexRequest):
    try:
        uuid = weaviate_client.index_document(
            request.content,
            request.media_type,
            request.source,
            request.metadata.get("image_base64") if request.metadata else None
        )
        return {"id": str(uuid), "status": "indexed"}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@app.post("/index_image")
async def index_image(file: UploadFile = File(...), source: str = ""):
    """Индексация изображения с описанием через BLIP"""
    try:
        # Сохраняем временно файл
        with open(f"temp_{file.filename}", "wb") as f:
            f.write(file.file.read())
        
        # Описываем через BLIP
        caption = image_processor.describe_image(f"temp_{file.filename}")
        
        # Кодируем изображение в base64
        with open(f"temp_{file.filename}", "rb") as f:
            image_base64 = base64.b64encode(f.read()).decode()
        
        # Индексируем
        uuid = weaviate_client.index_document(
            content=caption,
            media_type="image",
            source=source or file.filename,
            image_base64=image_base64
        )
        
        return {"id": str(uuid), "caption": caption}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

@app.get("/check")
async def root():
    return {"message": "API запущено и работает"}

@app.post("/search", response_model=List[SearchResult])
async def search(request: SearchRequest):
    try:
        results = weaviate_client.search(request.query, request.limit)
        return [SearchResult(**r) for r in results]
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))