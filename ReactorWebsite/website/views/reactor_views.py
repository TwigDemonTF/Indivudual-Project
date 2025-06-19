from django.http import JsonResponse, HttpResponse
from django.shortcuts import render

import requests

def reactor(request) -> HttpResponse:
    if 'reactorId' in request.session and request.session['reactorId'] != 0:
        return render(request, 'website/reactor/data.html')
    else:
        return render(request, 'website/reactor/reactor.html')

def reactor_latest_data(request):
    reactor_id = request.session.get('reactorId')
    token = request.session.get('token')

    if not reactor_id or not token:
        return JsonResponse({'error': 'No reactor ID or JWT token in session'}, status=400)

    try:
        headers = {
            'Authorization': f'Bearer {token}'
        }
        
        #  * call the https link directly instead of the http one because it doesnt work with the jwt
        res = requests.get(
            f"https://localhost:7083/api/Reactor/Latest?reactorId={reactor_id}",
            headers=headers,
            verify=False, 
            timeout=5
        )

        res.raise_for_status()
        data = res.json()
        return JsonResponse(data, safe=False)

    except requests.RequestException as e:
        return JsonResponse({'error': str(e)}, status=500)