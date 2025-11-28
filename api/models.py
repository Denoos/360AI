from pydantic import BaseModel
from typing import Optional

class SearchRequest(BaseModel):
    query: str
    search_type: str = "text_to_text"  # text_to_text, text_to_image, image_to_text
    limit: int = 10

class SearchResult(BaseModel):
    id: str
    content: str
    media_type: str
    relevance: float
    source: str

class IndexRequest(BaseModel):
    content: str
    media_type: str  # text, image, video_frame
    source: str
    metadata: Optional[dict] = None