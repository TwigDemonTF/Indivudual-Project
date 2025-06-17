from django.shortcuts import render
from django.http import HttpResponse
import requests

def reactor(request) -> HttpResponse:
    # check if user has a reactor
    if 'reactorId' in request.session:
        res = requests.get("http://localhost:5168/api/Reactor/Latest", verify=False)
        return render(request, 'website/reactor/data.html', context={})
    else:
        print(res)
    return render(request, 'website/reactor/reactor.html', context={})