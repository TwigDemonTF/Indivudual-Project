from flask import Flask, request, jsonify
import requests
import json

app = Flask(__name__)
@app.route('/chain', methods=["POST"])
def chain():
    data = request.get_json()
    print(data)
    requests.post("http://localhost:5168/api/Reactor", json=data, verify=False)
    return jsonify({'Status': 200})

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=8080, debug=True)