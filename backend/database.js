const sqlite3 = require('sqlite3').verbose();
let sql;


// Connect to DB
const db = new sqlite3.Database('./userdata.db', (error) => {
    if (error) {
        console.error('Error opening database:', error.message);
    } else {
        console.log('Connected to database.');

        // Create the users table
        db.run(`
            CREATE TABLE IF NOT EXISTS users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                username TEXT UNIQUE NOT NULL,
                email TEXT UNIQUE NOT NULL,
                password TEXT NOT NULL
            )
        `, (err) => {
            if (err) {
                console.error('Error creating table:', err.message);
                return;
            }

            console.log('Users Table Available');
            
            // Create test user
            db.run(`
                INSERT OR IGNORE INTO users (username, email, password)
                VALUES (?, ?, ?)
            `, ["Viper", "omarf.24@hotmail.com", "Delta3812"], function(err) {
                if (err) {
                    console.error('Insert error:', err.message);
                } else if (this.changes === 0) {
                    console.log('Test user already exists');
                } else {
                    console.log('Test user inserted with ID:', this.lastID);
                }
            });
        });
    }
});


module.exports = db;


        // db.run(`
        //     CREATE TABLE IF NOT EXISTS saves (
        //         id INTEGER PRIMARY KEY AUTOINCREMENT,
        //         )
        //     `);
        // db.run(`
        //     CREATE TABLE IF NOT EXISTS controls (
        //         id INTEGER PRIMARY KEY AUTOINCREMENT,
        //         )
        //     `);