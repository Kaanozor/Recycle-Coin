const {Blockchain, Transaction} = require('./BlockChain');

const express = require('express');
const app = express();
const port = 3000;
let recycleCoin = new Blockchain();


app.get('/transaction/user/:publicKey/:publickey1/:amount', (req,res) => {
    console.log(req.params)
    recycleCoin.createTransaction(new Transaction(req.params.publicKey,req.params.publicKey1, req.params.amount));
    res.send()
});




//total amount
app.get('/amount/:publicKey', (req,res) => {
    let amountt =recycleCoin.getBalanceOfAddress(req.params.publicKey);
    res.json(amountt)
});



app.get('/transaction/mine/:publicKey', (req,res) => {
    console.log(req.params)
    recycleCoin.minePendingTransactions(req.params.publicKey);

    res.send()
});

app.get('/transaction/:publicKey/:amount', (req,res) => {
    console.log(req.params)
    recycleCoin.createTransaction(new Transaction('operator',req.params.publicKey, req.params.amount));
    res.send()
});

app.get('/transaction/getall',(req,res)=>{
    res.json(recycleCoin.chain)
});
app.listen(port, () => {    
    console.log('Listening on port ' + port);

});
console.log("Transaction<.....");
recycleCoin.createTransaction( new Transaction('adress','adress2',100));
recycleCoin.minePendingTransactions('041982de774d48a6526a98d40db7efb14b5f555b40e114b9de9a71c3883a049e2df38417208a7350a11aa1542d4c60f49ae821b4c4b4d6cccccd0fa94072b7a87f');
recycleCoin.minePendingTransactions('041982de774d48a6526a98d40db7efb14b5f555b40e114b9de9a71c3883a049e2df38417208a7350a11aa1542d4c60f49ae821b4c4b4d6cccccd0fa94072b7a87f');
recycleCoin.minePendingTransactions('041982de774d48a6526a98d40db7efb14b5f555b40e114b9de9a71c3883a049e2df38417208a7350a11aa1542d4c60f49ae821b4c4b4d6cccccd0fa94072b7a87f');
recycleCoin.createTransaction(new Transaction('address2','alp-address', 110));
recycleCoin.createTransaction( new Transaction('adress','adress2',100));
recycleCoin.createTransaction( new Transaction('adress','adress2',100));

console.log('\n Starting the miner...');
console.log('\nbalance of alp is', recycleCoin.getBalanceOfAddress('alp-address'));
console.log("address3: "+recycleCoin.getBalanceOfAddress('address3'));

console.log('\nbalance of öz is', recycleCoin.getBalanceOfAddress('özcan'));

recycleCoin.minePendingTransactions('alp-address');
console.log("alp-address "+recycleCoin.getBalanceOfAddress('alp-address'));


console.log(JSON.stringify(recycleCoin,null,5));
/* recycleCoin.recycleCoinIslem(5);
console.log("mining 2");
recycleCoin.addBlock(new Block("05/04/2022",{userid:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));

recycleCoin.addBlock(new Block("05/04/2022",{amount:10}));
recycleCoin.addBlock(new Block(3,"05/04/2022",{amount:10}));
recycleCoin.addBlock(new Block(4,{amount:10,yunus:120}));
console.log(JSON.stringify(recycleCoin,null,4));*/
