import React from "react"; //類似jquery的包，一些API
// import ReactDOM from 'react-dom'; //將元件渲染到畫面上
import ReactDOM from "react-dom/client"; // react系統訊息建議的新版的寫法
import "./index.css";
import Home from "./pages/Home"; //首頁
import Page1 from "./pages/Page1";

// 舊式寫法
// ReactDOM.render(
//   <React.StrictMode>
//     <Home />
//   </React.StrictMode>,
//   document.getElementById('root')
// );

// react系統訊息建議的新版的寫法
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <Page1 />
    {/* <Home /> */}
  </React.StrictMode>
);

// webpack是前端專案管理器，協助打包整個專案用到的工具，也就是若你用一些很新的寫法而瀏覽器看不懂，則webpack會打包成瀏覽器看得懂的樣子(html css javascript)
// 此專案已用webpack包好，react開發團隊把很多檔案隱藏，npm run eject可以顯示隱藏內容
