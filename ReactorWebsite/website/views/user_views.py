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

def notification(request) -> HttpResponse:
        #delete the notification
        if request.method == "POST":
            try:
                notificationId = request.POST.get('notificationId')

                headers = {
                    'Authorization': f"Bearer {request.session.get('token')}"
                }

                res = requests.delete(
                    f"https://localhost:7083/Notification/{notificationId}",
                    headers=headers,
                    verify=False,
                    timeout=5
                )
            except requests.RecursionError as ex:
                return HttpResponse(f"Error deleting notification: {ex}", status=500)

        try:
            headers = {
                'Authorization': f'Bearer {request.session.get("token")}'
            }
            
            #  * call the https link directly instead of the http one because it doesnt work with the jwt
            res = requests.get(
                f"https://localhost:7083/Notification/{request.session.get('userId')}",
                headers=headers,
                verify=False, 
                timeout=5
            )
            res.raise_for_status()
            notifications = res.json()

            context = {
                'notifications': notifications,
            }

            return render(request, 'website/user/notifications.html', context=context)
        except requests.RequestException as ex:
            return HttpResponse(f"Error fetching notifications: {ex}", status=500)