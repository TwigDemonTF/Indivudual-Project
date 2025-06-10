from django.shortcuts import render
from django.http import HttpResponse
from typing import Any

import requests
import os

import urllib3
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)



# try:
#     res = requests.get("http://localhost:5168/User/Details/1", verify=False)
#     res = res.json()
#     print(res["minecraftUsername"])
# except requests.exceptions.RequestException as ex:
#     return HttpResponse(f"Error fetching data: {ex}")

def index(request) -> HttpResponse:
    context: dict[str, Any] = {}

    return render(request, 'website/main/index.html', context=context)
