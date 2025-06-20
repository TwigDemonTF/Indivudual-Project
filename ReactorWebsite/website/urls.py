# set patterns
from django.urls import path
from .views import main_views as main
from .views import user_views as user
from .views import reactor_views as reactor
from .ajax import BindToReactor, UpdateReactorValues

urlpatterns = [
    path('', main.index, name='index'),
    path('Login/', user.login, name='login'),
    path('Register/', user.register, name='register'),
    path('Logout/', user.logout, name='logout'),
    path('Notifications/', user.notification, name='notification'),

    path('Reactor/', reactor.reactor, name='reactor'),

    path('ajax/BindToReactor/', BindToReactor, name='BindToReactor'),
    path('ajax/UpdateReactorValues/', UpdateReactorValues, name="ChangeReactorValues"),

    path('reactor/latest/data', reactor.reactor_latest_data, name='reactor_latest_data'),
]