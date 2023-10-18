package myfunc

import "fmt"

func Gcd(m, n int) int {
	if n == 0 {
		return m
	} else {
		return Gcd(n, m%n)
	}
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
