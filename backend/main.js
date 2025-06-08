const express = require('express');
const app = express();
const cors = require('cors');
const PORT = 3000;

app.use(cors());
app.use(express.json());



const loginRoute = require('./function/login');
const registerRoute = require('./function/register');

app.use('/', loginRoute);
app.use('/register', registerRoute);



app.listen(
    PORT, 
    () => console.log(`Now listening on http://localhost:${PORT}`)
);
