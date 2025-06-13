# set patterns
from django.urls import path
from .views import main_views as main
from .views import user_views as user
from .views import reactor_views as reactor


urlpatterns = [
    path('', main.index, name='index'),
    path('Login/', user.login, name='login'),
    path('Register/', user.register, name='register'),
    path('Logout/', user.logout, name='logout'),

    path('Reactor/', reactor.reactor, name='reactor'),
]