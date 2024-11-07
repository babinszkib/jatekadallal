from flask import Flask, render_template
import sqlite3

app = Flask(__name__)

def get_db_connection():
    conn = sqlite3.connect('database.db')
    conn.row_factory = sqlite3.Row
    return conn

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/adatbazis')
def adatbazis():
    conn = get_db_connection()
    # adatbazibol beolvassa az adatokat
    played_time = conn.execute('SELECT played_time FROM statistics').fetchone()
    top_players = conn.execute('SELECT name FROM players ORDER BY score DESC LIMIT 3').fetchall()
    conn.close()
    return render_template('adatbazis.html', played_time=played_time, top_players=top_players)

if __name__ == '__main__':
    app.run(debug=True)
