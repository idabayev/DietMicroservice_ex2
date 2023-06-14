from flask import Flask, request, jsonify
from flask_pymongo import PyMongo, errors
from bson.json_util import dumps

app = Flask(__name__)
app.config["MONGO_URI"] = "mongodb://mongodb:27017/diets"
mongo = PyMongo(app)

@app.route('/diets', methods=['POST'])
def add_diet():
    if not request.is_json:
        return jsonify({'error': 'POST expects content type to be application/json'}), 415

    data = request.get_json()

    if not all(key in data for key in ('name', 'cal', 'sodium', 'sugar')):
        return jsonify({'error': 'Incorrect POST format'}), 422

    if mongo.db.diets.find_one({'name': data['name']}):
        return jsonify({'error': f'Diet with {data["name"]} already exists'}), 422

    mongo.db.diets.insert_one(data)
    return jsonify({'result': 'Diet added successfully'}), 201

@app.route('/diets', methods=['GET'])
def get_diets():
    diets = []
    for diet in mongo.db.diets.find():
        diets.append(diet)
    return dumps(diets), 200

@app.route('/diet/<name>', methods=['GET'])
def get_diet(name):
    diet = mongo.db.diets.find_one({'name': name})
    if diet is None:
        return jsonify({'error': 'Diet not found'}), 404
    return dumps(diet), 200

if __name__ == "__main__":
    app.run(host='0.0.0.0', port=80)
