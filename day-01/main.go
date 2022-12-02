package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println("Error opening file")
	}

	defer file.Close()

	const maxSize = 4

	scanner := bufio.NewScanner(file)

	var elfNumber int = 1
	var total int = 0
	food := make([]int, 0)

	for scanner.Scan() {
		calories, err := strconv.Atoi(scanner.Text())

		if err != nil {
			food = append(food, total)
			elfNumber++
			total = 0
		}

		total += calories
	}

	if err := scanner.Err(); err != nil {
		fmt.Println(err)
	}

	greatest := food[0]
	for i := 1; i < len(food); i++ {

		if food[i] > greatest {
			greatest = food[i]
		}
	}

	fmt.Println(food)
	fmt.Println(greatest)
}
