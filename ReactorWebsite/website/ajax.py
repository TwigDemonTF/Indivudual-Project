from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
from django.contrib.sessions.models import Session
import json
import requests

@csrf_exempt  # already gets done manually via JS header
def BindToReactor(request):
    if request.method == 'POST':
        try:
            data = json.loads(request.body)
            reactor_id = data.get('reactorId')
            user_id = request.session.get('userId')  # Grab userId from session

            if not reactor_id or not user_id:
                return JsonResponse({'success': False, 'message': 'Missing userId or reactorId'}, status=400)

            payload = {
                'userId': user_id,
                'reactorId': int(reactor_id)
            }

            # Call the .NET API (adjust port and URL as needed)
            response = requests.post("http://localhost:5168//User/BindReactor", json=payload, verify=False)

            if response.status_code == 200:
                request.session['reactorId'] = reactor_id
                return JsonResponse({'success': True})
            else:
                return JsonResponse({
                    'success': False,
                    'message': response.text
                }, status=response.status_code)

        except Exception as e:
            return JsonResponse({'success': False, 'message': str(e)}, status=500)

    return JsonResponse({'success': False, 'message': 'Invalid request method'}, status=405)

@csrf_exempt
def UpdateReactorValues(request):
    if request.method == 'POST':
        try:
            data = json.loads(request.body)
            reactorId = request.session.get('reactorId')
            token = request.session.get('token')
            if not reactorId or not token:
                return JsonResponse({'error': 'No reactor ID or JWT token in session'}, status=400)
            headers = {
                'Authorization': f"Bearer {token}"
            }

            print(data)

            payload = {
                'inputValue': data.get('input'),
                'outputValue': data.get('output'),
            }

            # call the API endpoint using PUT, not POST
            res = requests.put(
                f"https://localhost:7083/api/Reactor/{reactorId}",
                json=payload,
                headers=headers,
                verify=False,
                timeout=5,
            )
            res.raise_for_status()
            return JsonResponse({'success': True, 'message': 'Reactor values updated successfully'}, status=200)
        except requests.RequestException as ex:
            return JsonResponse({'error': str(ex)}, status=500)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
    return JsonResponse({'error': 'Invalid request method'}, status=400)

