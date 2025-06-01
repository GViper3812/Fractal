const express = require('express');
const app = express();
const PORT = 3000;

const db = require('./database');

app.use(express.json());

app.get('/login/:username', (req, res) => {
    const user = req.params.username
    const sql = `SELECT * FROM users WHERE username = ?`;
    db.get(sql, [user], (err, row) => {
        if (err) {
            return res.status(400).json({ error: err.message });
        }
        res.json(row);
        
    });
});

app.post('/test/:id', (req, res) => {

});

app.listen(
    PORT, 
    () => console.log(`Now listening on http://localhost:${PORT}`)
);
