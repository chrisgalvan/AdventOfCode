package main

import (
	"bufio"
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

type File struct {
	Name string
	Size int
}

type Folder struct {
	Parent  *Folder
	Name    string
	Size    int
	Files   map[string]*File
	Folders map[string]*Folder
}

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		panic(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	root := &Folder{Name: "/", Parent: nil, Size: 0, Folders: map[string]*Folder{}, Files: map[string]*File{}}
	node := root

	for scanner.Scan() {
		line := scanner.Text()
		tokens := strings.Fields(line)

		switch tokens[0] {
		case "$":
			// command
			if tokens[1] == "cd" {
				folderName := tokens[2]

				switch folderName {
				case "/":
					node = root
				case "..":
					node = node.Parent
				default:
					node = node.Folders[folderName]
				}
			}
		case "dir":
			// folder
			node.Folders[tokens[1]] = &Folder{Name: tokens[1], Parent: node, Size: 0, Folders: map[string]*Folder{}, Files: map[string]*File{}}
		default:
			// file
			node.Files[tokens[1]] = &File{Size: parseInt(tokens[0]), Name: tokens[1]}
			node.Size += parseInt(tokens[0])
			var parentNode = node.Parent
			for parentNode != nil {
				parentNode.Size += parseInt(tokens[0])
				parentNode = parentNode.Parent
			}
		}
	}

	directories := []Directory{}

	traverseFolders(root, &directories)

	availableSpace := 70000000 - root.Size
	spaceToRecover := 30000000 - availableSpace

	sort.Sort(Directories(directories))

	for _, dir := range directories {
		if dir.Size >= spaceToRecover {
			fmt.Printf("folder %s: %d size\n", dir.Name, dir.Size)
			break
		}
	}
}

func parseInt(val string) int {
	v, _ := strconv.ParseInt(val, 10, 64)
	return int(v)
}

func traverseFolders(node *Folder, list *[]Directory) {
	for _, f := range node.Folders {
		traverseFolders(f, list)
		*list = append(*list, Directory{Name: f.Name, Size: f.Size})
	}
}

type Directory struct {
	Name string
	Size int
}

type Directories []Directory

func (d Directories) Less(i, j int) bool {
	return d[i].Size < d[j].Size
}

func (d Directories) Swap(i, j int) {
	d[i], d[j] = d[j], d[i]
}

func (d Directories) Len() int {
	return len(d)
}
