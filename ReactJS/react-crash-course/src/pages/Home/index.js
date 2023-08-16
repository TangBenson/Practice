//Home的根元件
import { useState, useEffect } from "react"; //{}內表示變數，讓Home的狀態共享給Edit和List
import List from "./components/List";
import Edit from "./components/Edit";
import "./index.css";
import { API_GET_DATA } from "../../global/constants";

// 可以這樣定義css
const app = {
  color: "red",
};

// promise的包裝，避免寫很多callback地獄
async function fetchData(setData) {
  const res = await fetch(API_GET_DATA)
  const { data } = await res.json()
  setData(data)
  console.log(data);
}

async function fetchSetData(data) {
  await fetch(API_GET_DATA, {
    method: "PUT",
    headers: {
      'Content-type': 'application/json'
    },
    body: JSON.stringify({ data })
  })
}

// 寫元件，就是寫個function再export出去，而react怎知這是一般function還是react的元件呢? 重點在return jsx
const Home = () => {

  //useState狀態有改變，react就知道要重新渲染。這是在父層給Edit和List共用的狀態
  //宣告一個陣列變數放入State狀態，才會和react渲染機制綁定，setData則會賦值給data
  const [data, setData] = useState([]);

  // useEffect表示狀態變動後要去做啥事
  // 綁訂data(依賴關係)，當data變動則func被執行。渲染第一次時也會執行。外面的func是每次執行會做的事情，裡面的func是每次渲染結束要開始下一次渲染之前會做的事情
  useEffect(() => {
    //綁訂的事情
    console.log('耶果奶昔')
    return () => {
      //取消綁訂的事情
      console.log('小小樹食')
    }
  }, [data])
  // 若不綁訂data([]內為空)則只會執行一次，data如何變都不會執行
  useEffect(() => {
    console.log('全真素排飯')
  }, [])
  // 若不建立依賴(,[]全部刪掉)，則每次都會執行
  useEffect(() => {
    console.log('12月沒事')
  })

  // 模擬一登入頁面就打API
  useEffect(() => {
    fetchData(setData)
    // #region promise寫法
    // fetch(API_GET_DATA)
    //   .then(res => res.json())
    //   .then(data => {
    //     setData(data)
    //     console.log(data)
    //   })
    // #endregion
  }, [])
  useEffect(() => {
    fetchSetData(data)
  }, [data])

  // 這邊看起來像html，但其實是jsx，用寫html方式建立react的物件
  return (
    <div style={app} className="app">
      <Edit add={setData} />
      <List listData={data} deleteD={setData} />  {/* 把listData傳給List，React術語稱為props */}
    </div>
  );
};

export default Home;