const express = require('express');
const app = express();
const cors = require('cors');
const PORT = 3000;

const db = require('./database');

app.use(cors());
app.use(express.json());

app.get('/login/:username/:password', (req, res) => {
    const user = req.params.username
    const pass = req.params.password
    const sql = `SELECT * FROM users WHERE username = ? and password = ?`;
    db.get(sql, [user, pass], (err, row) => {
        if (err) {
            return res.status(400).send("Please fill all fields");
        }
        if (!row) {
            return res.status(404).send("Invalid Credentials");
        }
        res.send("Login Confirmed");
    });
});

app.post('/test/:id', (req, res) => {

});

app.listen(
    PORT, 
    () => console.log(`Now listening on http://localhost:${PORT}`)
);
