from django.shortcuts import render
from django.http import HttpResponse
from typing import Any
from django.shortcuts import redirect

import requests

def register(request) -> HttpResponse:
    if request.method == 'POST':
        data = {
            "Email": request.POST.get('email'),
            "Password": request.POST.get('password'),
            "MinecraftUsername": request.POST.get('minecraftUsername'),
            "ReactorId": 0,
        }
        res = requests.post("http://localhost:5168/User/Register", json=data, verify=False)
        if res.status_code == 200:
            return render(request, 'website/main/index.html', context={})
    return render (request, 'website/user/register.html')

def login(request) -> HttpResponse:
    if request.method == "POST":
        try:
            formData = request.POST
            data: dict[str, Any] = {
                'Email': formData.get("email"),
                'Password': formData.get("password"),
            }
            res = requests.post("http://localhost:5168/User/Login", json=data, verify=False)
            if res.status_code == 200:
                request.session['minecraftUsername'] = res.json()['user']['minecraftUsername']
                request.session['email'] = res.json()['user']['email']
                if res.json()['user']['reactorId'] != 0:
                    print(res.json()['user']['reactorId'])
                    request.session['reactorId'] = res.json()['user']['reactorId']

                context = {
                    'minecraftUsername': request.session['minecraftUsername'],
                }
                return render(request, 'website/main/index.html', context=context)
        except requests.exceptions.RequestException as ex:
            return HttpResponse(f"Error Logging in: {ex}")
    else:
        return render (request, 'website/user/login.html')

def logout(request) -> HttpResponse:
    request.session.flush()
    return render(request, 'website/main/index.html', context={})