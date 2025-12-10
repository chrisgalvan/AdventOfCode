import java.io.File

fun main() {
    val filePath = "./day-07/src/input.txt"

    val matrix = File(filePath).readLines().map { it.toList() }

    partOne(matrix)
    partTwo(matrix)
}

fun partOne(matrix: List<List<Char>>) {
    val beams = matrix.first().toCharArray()
    var counter = 0
    matrix.forEachIndexed { i, row ->
        row.forEachIndexed { j, cell ->
            when (cell) {
                'S' -> beams[j] = '|'
                '^' ->
                    if (beams[j] == '|') {
                        beams[j] = '.'
                        if (j > 0) beams[j-1] = '|'
                        if (j < beams.size - 2) beams[j+1] = '|'
                        counter++
                    }
            }
        }
    }

    println(counter)
}

fun partTwo(matrix: List<List<Char>>) {
    val beams = Array<Long>(matrix.first().size) { 0 }
    var counter = 0
    matrix.forEachIndexed { i, row ->
        row.forEachIndexed { j, cell ->
            when (cell) {
                'S' -> beams[j] = 1
                '^' ->
                    if (beams[j] > 0) {
                        beams[j-1] += beams[j]
                        beams[j+1] += beams[j]
                        beams[j] = 0
                        counter++
                    }
            }
        }
    }

    println(beams.sumOf { it })
}