const express = require('express');
const cors = require('cors');
const app = express();
const PORT = 3000;

app.use(cors());

app.get('/api/message', (req, res) => {
  res.json({ message: 'Hello from Node.js backend! Divijesh Varma' });
});

app.listen(PORT, () => {
  console.log(`Node.js backend running on http://localhost:${PORT}`);
});

