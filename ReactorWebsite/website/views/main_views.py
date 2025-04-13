from django.shortcuts import render
from django.http import HttpResponse

import os

def index(request) -> HttpResponse:
    return render(request, 'website/main/index.html', context={})
