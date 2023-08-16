import { useState } from "react";
import { v4 } from "uuid";

const Edit = ({ add }) => {
  //Edit自己的狀態是為了控制元件，然後把元件value取出去set Home的狀態
  const [note, setNote] = useState("");
  function noteChage(e) {
    setNote(e.target.value);
  }

  const [date, setDate] = useState("");
  function dateChage(e) {
    setDate(e.target.value);
  }

  const [time, setTime] = useState("");
  function timeChage(e) {
    setTime(e.target.value);
  }

  function addItem() {
    // add([1,2,3])
    add(function (prevDataQQ) {
      return [
        // ...prevDataQQ, //解構的陣列放這，每次新增的項目就會在最下面
        {
          id: v4(), //引入uuid包即可自動針對每筆資料產生 unique 的 ID
          note,
          date,
          time,
        },
        ...prevDataQQ, //解構的陣列放這，每次新增的項目就會在最上面
      ];
    });
  }

  return (
    <div>
      <h1>備忘錄</h1>
      <p>活動名稱：</p>
      <input type="text" value={note} onChange={noteChage} />
      <p>日期：</p>
      <input type="date" value={date} onChange={dateChage} />
      <p>領隊：</p>
      <input type="text" value={time} onChange={timeChage} />
      <button className="add" onClick={addItem}>
        新增
      </button>
    </div>
  );
};

export default Edit;