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
            return redirect('/Login')
    return render (request, 'website/user/register.html')


def login(request):
    if request.method == "POST":
        try:
            formData = request.POST
            data = {
                'Email': formData.get("email"),
                'Password': formData.get("password"),
            }

            res = requests.post("http://localhost:5168/User/Login", json=data, verify=False)

            if res.status_code == 200:
                res_data = res.json()
                user = res_data['user']
                token = res_data['token']
                print(user)
                request.session['token'] = token
                request.session['userId'] = user['id']
                request.session['email'] = user['email']
                request.session['minecraftUsername'] = user['minecraftUsername']
                request.session['reactorId'] = user['reactorId']
                
                if user['reactorId'] != 0:
                    request.session['reactorId'] = user['reactorId']

                return render(request, 'website/main/index.html', context={
                    'minecraftUsername': user['minecraftUsername']
                })

            return HttpResponse(f"Login failed: {res.text}", status=res.status_code)

        except requests.exceptions.RequestException as ex:
            return HttpResponse(f"Error Logging in: {ex}")

    return render(request, 'website/user/login.html')

def logout(request) -> HttpResponse:
    request.session.flush()
    return render(request, 'website/main/index.html', context={})