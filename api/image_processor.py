# image_processor.py
from transformers import BlipProcessor, BlipForConditionalGeneration
from PIL import Image
import requests

class ImageProcessor:
    def __init__(self):
        self.processor = BlipProcessor.from_pretrained("Salesforce/blip-image-captioning-base")
        self.model = BlipForConditionalGeneration.from_pretrained("Salesforce/blip-image-captioning-base")
    
    def describe_image(self, image_path: str) -> str:
        """Описывает изображение через BLIP"""
        image = Image.open(image_path).convert('RGB')
        
        inputs = self.processor(image, return_tensors="pt")
        out = self.model.generate(**inputs, max_new_tokens=50)
        caption = self.processor.decode(out[0], skip_special_tokens=True)
        
        return caption