const express = require('express');
const router = express.Router();
const db = require('../database');

router.post('/login', (req, res) => {
    const { username, password } = req.body;

    if (!username || !password) {
        return res.status(400).json({
            success: false,
            errorType: "missing_fields",
            message: "Please fill all fields"
        });
    }

    const sql = `SELECT * FROM users WHERE username = ? AND password = ?`;

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

module.exports = router;
