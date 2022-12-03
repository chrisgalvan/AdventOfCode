package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println("Error opening file")
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	const (
		Rock     = 1
		Paper    = 2
		Scissors = 3
	)

	my_map := map[string]int{
		"A": Rock,
		"B": Paper,
		"C": Scissors,
		"X": Rock,
		"Y": Paper,
		"Z": Scissors,
	}

	total := 0
	result := 0

	for scanner.Scan() {
		line := strings.Split(scanner.Text(), " ")

		result = (3 + my_map[line[0]] - my_map[line[1]]) % 3

		switch result {
		case 0:
			total += 3 // tie
		case 1:
			total += 0 // lose
		case 2:
			total += 6 // win
		}

		total += my_map[line[1]]
	}

	fmt.Println(total)
}
