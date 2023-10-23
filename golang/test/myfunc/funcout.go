package myfunc

import "fmt"

func Sum(numbers ...int) int {
	var sum int
	for _, number := range numbers {
		sum += number
	}
	return sum
}

func Test(n1 int, n2 int) (int, string) {
	return n1 * n2, "ddd"
}

func Test2() {
	//defer 會等到周圍的 function 都執行完成後，再執行
	fmt.Println("counting")

	for i := 0; i < 5; i++ {
		defer fmt.Println(i)
	}

	fmt.Println("done")
}

type Person struct {
	Name string
	Age  int
}
