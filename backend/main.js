const express = require('express');
const app = express();
const cors = require('cors');
const PORT = 3000;

const db = require('./database');

app.use(cors());
app.use(express.json());

app.post('/login', (req, res) => {
    const { username, password } = req.body;

    if (!username || !password) {
        return res.status(400).json({
            success: false,
            errorType: "missing_fields",
            message: "Please fill all fields"
        });
    }

    const sql = `SELECT * FROM users WHERE username = ? and password = ?`;
    
    db.get(sql, [username, password], (err, row) => {
        if (err) {
            return res.status(500).json({
                success: false,
                errorType: "database_error",
                message: "Server Error"
            });
        }

        if (!row) {
            return res.status(404).json({
                success: false,
                errorType: "invalid_credentials",
                message: "Invalid Credentials"
            });
        }

        res.status(200).json({
            success: true,
            errorType: null,
            message: "Login Confirmed"
        });
    });
});

app.post('/test/:id', (req, res) => {

});

app.listen(
    PORT, 
    () => console.log(`Now listening on http://localhost:${PORT}`)
);
