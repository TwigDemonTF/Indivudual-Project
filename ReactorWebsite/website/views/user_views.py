from django.shortcuts import render
from django.http import HttpResponse


def register(request) -> HttpResponse:
    return render (request, 'website/user/register.html')

def login(request) -> HttpResponse:
    return render (request, 'website/user/login.html')

def logout(request) -> HttpResponse:
    pass