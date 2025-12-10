import java.io.File
import java.nio.file.Files.lines

val delimiter = "\\s+".toRegex()

fun main() {
    val filePath = "./day-06/src/input.txt"

    partOne(filePath)
    partTwo(filePath)
}

fun partOne(filePath: String) {

    val lines = File(filePath).readLines()

    val operators = lines.last().split(delimiter)

    var totals = lines[0].split(delimiter).filter {it.isNotEmpty() }.map { it.toLong() }.toMutableList()

    (1 .. lines.size - 2).forEach { i ->
        val line = lines[i]

        val values = line.split(delimiter).filter {it.isNotEmpty() }.map { it.toLong() }

        values.forEachIndexed { i, n ->
            if (operators[i] == "+") totals[i] += n
            if (operators[i] == "*") totals[i] *= n
        }
    }

    var counter = totals.sumOf { it }

    println(counter)
}

fun partTwo(filePath: String) {
    val matrix = File(filePath)
        .readLines()
        .map { it.toList()}

    val list = mutableListOf<Long>()
    var total = 0L
    var op = '.'

    for (col in matrix[0].indices) {
        val bottom = matrix.last()[col]
        if (bottom == '+' || bottom == '*') op = bottom

        val digits = matrix.dropLast(1).map { it[col] }

        val number = digits
            .filter { it.isDigit() }
            .fold(0L) { acc, c ->
                acc * 10 + c.digitToInt()
            }

        if (digits.all { it == ' ' }) {
            list.add(total)
            total = 0
        }

        when (op) {
            '+' -> total += number
            '*' -> total = if (total == 0L) number.toLong() else total * number
        }
    }

    list.add(total)
    println(list.sumOf { it })
}