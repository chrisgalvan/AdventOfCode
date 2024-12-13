package main

import (
	"adventofcode/2024/utils"
	"fmt"
	"strconv"
)

type Point struct {
	y int
	x int
}

const (
	Up = iota
	Right
	Down
	Left
)

func main() {
	var lines []string
	startingPoint := Point{x: 0, y: 0}
	utils.StreamFile("input.txt", func(line string) {
		for i, char := range line {
			if char == '^' {
				startingPoint.x = i
				startingPoint.y = len(lines)
			}
		}
		lines = append(lines, line)
	})

	one(lines, startingPoint)
}

func one(lines []string, currentPoint Point) {
	m := make(map[string]int)
	direction := Up
	exit := false

	for !exit {
		nextPoint := currentPoint

		// calculate next coordinates
		switch direction % 4 {
		case Up:
			nextPoint.y -= 1
		case Right:
			nextPoint.x += 1
		case Down:
			nextPoint.y += 1
		case Left:
			nextPoint.x -= 1
		}

		// check for boundaries
		if nextPoint.y >= len(lines) || nextPoint.x >= len(lines[0]) || nextPoint.y < 0 || nextPoint.x < 0 {
			exit = true
			continue
		}

		// check if blocked
		if lines[nextPoint.y][nextPoint.x] == '#' {
			direction += 1
			continue
		}

		if lines[currentPoint.y][currentPoint.x] == '.' || lines[currentPoint.y][currentPoint.x] == '^' {
			key := strconv.Itoa(currentPoint.y) + "," + strconv.Itoa(currentPoint.x)
			m[key] += 1
		}

		currentPoint = nextPoint
	}

	fmt.Printf("One result: %d \n", len(m)+1)
}

func two() {

}
