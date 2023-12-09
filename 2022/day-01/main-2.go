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

	var topThree [3]int
	// greatest := food[0]
	for i := 0; i < len(food); i++ {
		if food[i] > topThree[0] {
			topThree[0] = food[i]
		} else if food[i] > topThree[1] {
			topThree[1] = food[i]
		} else if food[i] > topThree[2] {
			topThree[2] = food[i]
		}
	}

	fmt.Println(topThree)
	fmt.Println(topThree[0] + topThree[1] + topThree[2])
}
