from flask import Flask, request, jsonify
from flask_pymongo import PyMongo
from bson.json_util import dumps

app = Flask(__name__)
app.config["MONGO_URI"] = "mongodb://localhost:27017/diets"
mongo = PyMongo(app)

@app.route('/diets', methods=['POST'])
def add_diet():
    data = request.get_json()
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
    return dumps(diet), 200

if __name__ == "__main__":
    app.run(host='0.0.0.0', debug=True)