//js的class只算是個語法糖，不算是真的class，充其量是讓其他語言者轉過來時比較好適應，不然構造函數的用法會不適應

// 構造函數 function 
// function createCard(initName) {
//   this.name = initName
// }

// class的基本操作
// class Card {
//   constructor(initName) {
//     this.name = initName
//   }
// }

// const c1 = new Card("布魯斯")
// console.log('c1', c1)
// console.log(c1.name);

// this，有些許別於其他語言，取決於當下執行的是誰，this就是誰
// class Card {
//   constructor(initName) {
//     this.name = initName
//     // this
//     // this.hellooo = this.hello.bind(this)
//   }

//   hello = () => {
//     console.log("hello", this.name)
//   }
// }

// const c1 = new Card("布魯斯")
// console.log('c1', c1)
// c1.hello()

// const a = { name: 'AA' }
// a.hellooooo = c1.hello
// a.hellooooo()


// 繼承
class Car {
  constructor(initName) {
    this.name = initName
  }
  start() {
    // TODO del
    console.log('車子啟動')
  }
}

class Porshe extends Car {
  constructor(namePorshe) {
    super(namePorshe)
  }

  // start2() {
  //   super.start()
  //   console.log('車子排氣')
  // }

  // start() {
  //   super.start()
  //   console.log('porshe車子啟動')
  // }
}

const p1 = new Porshe("布魯斯的保時捷")
p1.start()
// TODO del
console.log('name', p1.name)
