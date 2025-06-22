from flask import Flask, request, jsonify
import json
import os
import re
import urllib.parse
import requests
from difflib import get_close_matches


app = Flask(__name__)


# memory
DB_FILE = "chatbot_memory.json"
if os.path.exists(DB_FILE):
    with open(DB_FILE, "r", encoding="utf-8") as f:
        memory = json.load(f)
else:
    memory = {}


# Utilities
def spell_check(query, possibilities):
    matches = get_close_matches(query, possibilities, n=1, cutoff=0.6)
    return matches[0] if matches else query


def extract_expression(text):
    match = re.search(r"(\d+\s*[\+\-\*/]\s*\d+)", text)
    return match.group(1) if match else None


def try_math(expr):
    try:
        expr = expr.strip().replace("=", "")
        allowed = "0123456789+-*/(). "
        if all(c in allowed for c in expr):
            result = eval(expr)
            return f"The answer is: {result}"
        return None
    except:
        return None


def get_web_answer(question):
    try:
        url = f"https://api.duckduckgo.com/?q={urllib.parse.quote_plus(question)}&format=json&no_html=1"
        response = requests.get(url, timeout=5)
        data = response.json()
        if data.get("AbstractText"):
            return f"According to {data.get('AbstractSource', 'DuckDuckGo')}: {data['AbstractText']}"
        if data.get("RelatedTopics"):
            for topic in data['RelatedTopics']:
                if 'Text' in topic:
                    return topic['Text']
        return None
    except:
        return None


def get_openlibrary_info(query):
    try:
        url = f"https://openlibrary.org/search.json?q={urllib.parse.quote_plus(query)}"
        resp = requests.get(url, timeout=5).json()
        if resp["docs"]:
            book = resp["docs"][0]
            return f"{book.get('title')} by {book.get('author_name', ['Unknown'])[0]}"
        return None
    except:
        return None

def generate_search_links(question):
    encoded = urllib.parse.quote_plus(question)
    return {
        'Google': f'https://www.google.com/search?q={encoded}',
        'DuckDuckGo': f'https://duckduckgo.com/?q={encoded}',
        'YouTube': f'https://www.youtube.com/results?search_query={encoded}'
    }


def smart_answer(question):
    extracted = extract_expression(question)
    for method in [lambda q: try_math(extracted if extracted else q), get_web_answer, get_openlibrary_info]:
        answer = method(question)
        if answer:
            return answer
    return "I couldn't find a definitive answer to that question."


# Flask End
@app.route("/")
def hello():
    return "Hello World!"

@app.route("/chatbot", methods=["GET", "POST"])
def api_chatbot():
    if request.method == "GET":
        return """
        <h2>Chatbot Endpoint</h2>
        <p>This endpoint requires a POST request with JSON like:</p>
        <pre>{ "question": "What is Python?" }</pre>
        """

    data = request.get_json()
    question = data.get("question", "").strip()
    if not question:
        return jsonify({"error": "Question is required."}), 400

    key = question.lower()
    answer = memory.get(key)

    if not answer:
        answer = smart_answer(question)

    links = generate_search_links(question)
    return jsonify({"answer": answer, "links": links})

# Serverrrrrr
if name == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
