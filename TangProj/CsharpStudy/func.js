// javascript定義function的方式

function aa(b,c){
    return b*c/2;
}
console.log(aa(5,2))

var aa = new Function('b','c','return b*c/2;')
console.log(aa(5,2))

//省略new視為全域函數
var aa = Function('b','c','return b*c/2;')
console.log(aa(5,2))

var aa = function(b,c){
    return b*c/2;
}

let aa = (b,c)=>{
    return b*c/2;
}

let aa = (b,c)=>b*c/2;

let aaa = b=>b*5/2;

let aaaa = ()=> console.log('hihihi')