from django.shortcuts import render
from django.http import HttpResponse

def reactor(request) -> HttpResponse:
    return render(request, 'website/reactor/reactor.html', context={})