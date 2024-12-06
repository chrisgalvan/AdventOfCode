package main

import (
	"adventofcode/2024/utils"
	"fmt"
)

func main() {
	var matrix []string
	utils.StreamFile("input.txt", func(line string) {
		matrix = append(matrix, line)
	})

	one(matrix)
	two(matrix)
}

func one(lines []string) {
	total := 0
	fmt.Printf("One - Result %d\n", total)
}

func two(lines []string) {
	total := 0
	fmt.Printf("Two - Result %d\n", total)
}
