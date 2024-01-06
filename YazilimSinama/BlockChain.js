const SHA256 = require('crypto-js/sha256');// crypto-js kütüphanesi kullanılıyor hashleme algoritmasında kullanılmak için

class Transaction{
    constructor(fromAddress, toAddress, amount){
        this.fromAddress=fromAddress;
        this.toAddress=toAddress;
        this.amount=amount;
    }
    calculateHash(){
        return SHA256(this.fromAddress+this.toAddress+this.amount).toString();
    }
    signTransaction(signingKey){
        if (signingKey.getPublic('hex')!== this.fromAddress) {
            throw new console.error();
        }
        const hashTx = this.calculateHash();
        const sig = signingKey.sign(hashTx, 'base64');
        this.signature = sig.toDER('hex');
    }

}
class Block{
    constructor(timestamp,transactions,previousHash =''){
        this.timestamp=timestamp;//ne zaman oluşturulduğunu söyler
        this.transactions=transactions;//ne kadar coin gönderildiği kim tarafından gönderildiği gibi blockchain ile ilişkili verileri tutar
        this.previousHash=previousHash;//oluşturulan block'un kendisinden önceki block'u göstermesini sağlar
        this.hash=this.calculateHash();//hash hesaplamasında kullanılacak
        this.nonce=0;//mining zorluğu oluşturulurken kullanılır
    }
    calculateHash(){//hash karıştırmasında hazır kütüphaneden sha256'yı kullanıyoruz

        return SHA256(this.previousHash+this.timestamp+JSON.stringify(this.transactions)+this.nonce).toString();

    }
    mineBlock(difficulty){
    while(this.hash.substring(0,difficulty) !== Array(difficulty + 1).join("0")){
    this.nonce++;
    this.hash = this.calculateHash();
    }
    //postgres Blok bilgilerini al database'e yolla
    console.log("Block mined: "+this.hash);
    

}

   
}
class Blockchain{
    constructor(){
        this.chain=[this.createGenesisBlock()];//ilk oluşturulan block bu bloğa genel olarak genesis bloğu denir 
        this.difficulty=1;//zorluk derecesi eklenir.burada verilen değer hash değerinin başında kaç tane sıfır olacağını belirler
        this.pendingTransactions=[];//transactionda tutulan değerler bir sonraki bloğa aktarılmak için burada tutulur. 
        this.miningReward=100;//başarılı oluşturulan her blokta verilecek ödül miktarı;
    }
    createGenesisBlock(){//ilk blok oluşturuldu
        return new Block("01/01/2022","genesis block","0");
    }
    //getLatestBlock = () => this.chain[this.chain.length-1];
    getLatestBlock(){//son bloğu getirir
        return this.chain[this.chain.length-1];
    }//düzenle
    supplyIsOver(){
        if (this.chain.length==3) {
        return true;    
        }
        else
        return false;
    }
    

    // postgres ödülü kimin aldığını yolla 
       minePendingTransactions(miningRewardAddress){
        let block = new Block(Date.now(), this.pendingTransactions);
        block.mineBlock(this.difficulty);
        this.chain.push(block);

        this.pendingTransactions = [
            new Transaction(null, miningRewardAddress, this.miningReward)
        ];
             
        if (this.supplyIsOver()==true) {
            console.log("Coin supply is done");
        }

    }

    createTransaction(transaction){
        this.pendingTransactions.push(transaction);

    }

    // api kullanıcının ne kadar parası olduğunu gösterir 
    getBalanceOfAddress(address){
        let balance =0;

        for (const block of this.chain) {
            for (const trans of block.transactions){
                if(trans.fromAddress == address){
                    balance -=trans.amount;
                }
                
                if (trans.toAddress == address) {
                    balance +=trans.amount;
                    
                }
            }
        }
        return balance;

    }
    /* addBlock(newBlock){//
        newBlock.previousHash=this.getLatestBlock().hash;//yeni bloğa eski blok hashi eşitlenir
        newBlock.mineBlock(this.difficulty);
        //newBlock.hash=newBlock.calculateHash();//yeni blok için hash oluşturulur
        this.chain.push(newBlock);
    } */
   
}
module.exports.Blockchain = Blockchain;
module.exports.Transaction = Transaction;