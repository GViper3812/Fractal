const express = require('express');
const router = express.Router();
const db = require('../database');

router.post('/check', (req, res) => {
    const { username, email } = req.body;

    if (!username || !email) {
        return res.status(400).json({
            success: false,
            errorType: "missing_fields",
            message: "Please provide both username and email"
        });
    }

    const checkSql = `SELECT username, email FROM users WHERE username = ? OR email = ?`;

    db.all(checkSql, [username, email], (err, rows) => {
        if (err) {
            return res.status(500).json({
                success: false,
                errorType: "database_error",
                message: "Server error during check"
            });
        }

        let usernameTaken = false;
        let emailTaken = false;

        for (const row of rows) {
            if (row.username === username) usernameTaken = true;
            if (row.email === email) emailTaken = true;
        }

        if (usernameTaken && emailTaken) {
            return res.status(409).json({
                success: false,
                errorType: "username_and_email_taken",
                message: "Username and email are already registered"
            });
        }

        if (usernameTaken) {
            return res.status(409).json({
                success: false,
                errorType: "username_taken",
                message: "Username is already registered"
            });
        }

        if (emailTaken) {
            return res.status(409).json({
                success: false,
                errorType: "email_taken",
                message: "Email is already registered"
            });
        }

        res.status(200).json({
            success: true,
            errorType: null,
            message: "Username and email are available"
        });
    });
});

router.post('/confirm', (req, res) => {
    const { username, email, password } = req.body;

    const insertSql = `INSERT INTO users (username, email, password) VALUES (?, ?, ?)`;

    db.run(insertSql, [username, email, password], function (err) {
        if (err) {
            return res.status(500).json({
                success: false,
                errorType: "database_error",
                message: "Registration failed"
            });
        }

        res.status(201).json({
            success: true,
            errorType: null,
            message: "Registration confirmed",
            id: row.id
        });
    });
});

module.exports = router;
