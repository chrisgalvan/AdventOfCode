package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strings"

	"adventofcode/2024/utils"
)

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		panic(err)
	}

	defer file.Close()

	var lines [][]int

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := strings.Split(scanner.Text(), " ")

		var numbers []int
		for _, v := range line {
			numbers = append(numbers, utils.ToInt(v))
		}
		lines = append(lines, numbers)
	}

	one(lines)
	two(lines)
}

func one(lines [][]int) {
	totalSafe := 0

	for _, row := range lines {
		result, _ := isSafe(row)
		if result {
			totalSafe++
		}
	}

	fmt.Printf("One - Result: %d\n", totalSafe)
}

func two(lines [][]int) {
	totalSafe := 0

	for _, row := range lines {
		result, _ := isSafe(row)
		if result {
			totalSafe++
		}
	}

	fmt.Printf("Two - Result: %d\n", totalSafe)
}

func isSafe(line []int) (bool, int) {
	prev := line[0]
	asc := line[1] > prev
	for i := 1; i < len(line); i++ {
		current := line[i]
		if (current == prev) || (current > prev && !asc) || (current < prev && asc) || (math.Abs(float64(current)-float64(prev)) > 3) {
			return false, i
		}
		prev = current
	}

	return true, 0
}
