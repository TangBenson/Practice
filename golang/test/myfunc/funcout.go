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
	//defer 會等到周圍的 function 都執行完成後，return 之前再執行
	fmt.Println("A")

	for i := 0; i < 2; i++ {
		defer fmt.Println(i)
	}

	fmt.Println("B")
}

type Person struct {
	Name string
	Age  int
}

type Animal interface {
	Eat()
	Run()
}

type Dog struct {
	Name string
}

func (d *Dog) Eat() {
	fmt.Printf("%s is eating\n", d.Name)
}

func (d *Dog) Run() {
	fmt.Printf("%s is running\n", d.Name)
}

func ShowEat(animal Animal) {
	animal.Eat()
}

func ShowRun(animal Animal) {
	animal.Run()
}
