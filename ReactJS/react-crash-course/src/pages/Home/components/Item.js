
const Item = ({ id, note, date, time, deleteData }) => {

  //用過濾的方式，達成刪除效果
  function deleteItem() {
    deleteData(function (prev) {
      return prev.filter(item => item.id !== id);
    });
  }

  return (
    <div className="item">
      <div>
        <p>{note}</p>
        <p>{`${date} ${time}`}</p>
      </div>
      <button onClick={deleteItem} className="remove">
        刪除
      </button>
    </div>
  );
};

export default Item;