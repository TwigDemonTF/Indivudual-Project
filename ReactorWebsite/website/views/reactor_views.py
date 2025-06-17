from django.shortcuts import render
from django.http import HttpResponse
import requests

def reactor(request) -> HttpResponse:
    # check if user has a reactor
    if 'reactorId' in request.session:
        pass
    else:
        res = requests.get("http://localhost:5168/User/BoundReactor", verify=False)
        pass
    return render(request, 'website/reactor/reactor.html', context={})