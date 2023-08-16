import Item from "./Item";

const List = ({ listData, deleteD }) => {
  return (
    <div className="list">
      {
        listData.map(item => {
          const { note = 'undefinded下的預設值', date, time, id } = item; //用解構賦值
          return (
            <Item
              key={id} //react建議要給key值，為了效能優化，不加這行也可以run，但Console會噴錯誤，噴屁噴
              id={id}
              note={note} //左邊note表示props要取值的東西，是Item裡的note
              date={date}
              time={time}
              deleteData={deleteD}
            />);
        })
      }
    </div>
  );
};

export default List;