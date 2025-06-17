from django.http import JsonResponse, HttpResponse
from django.shortcuts import render

import requests

def reactor(request) -> HttpResponse:
    if 'reactorId' in request.session:
        return render(request, 'website/reactor/data.html')
    else:
        return render(request, 'website/reactor/reactor.html')

def reactor_latest_data(request):
    reactor_id = request.session.get('reactorId')
    if not reactor_id:
        return JsonResponse({'error': 'No reactor ID in session'}, status=400)

    try:
        res = requests.get(
            f"http://localhost:5168/api/Reactor/Latest?reactorId={reactor_id}",
            verify=False,
            timeout=5
        )
        res.raise_for_status()
        data = res.json()
        print(data)
        return JsonResponse(data, safe=False)
    except requests.RequestException as e:
        return JsonResponse({'error': str(e)}, status=500)