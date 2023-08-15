import C from './c'

const arr = [1, 2, 3]

const B = () => {
    return <div>
        {
            arr.map(item => <C/>)
        }
        {
            arr.map(item => <div>{item}</div>)
        }
    </div>
}

export default B


//想要在jsx寫js都要加個大括號{}