package mx.com.firebasemenudecomidas.dummy

import java.util.ArrayList
import java.util.HashMap

/**
 * Helper class for providing sample content for user interfaces created by
 * Android template wizards.
 *
 * TODO: Replace all uses of this class before publishing your app.
 */
object DummyContent {

    /**
     * An array of sample (dummy) items.
     */
    val ITEMS: MutableList<Comida> = ArrayList()

    /**
     * A map of sample (dummy) items, by ID.
     */
    val ITEM_MAP: MutableMap<String, Comida> = HashMap()

    private val COUNT = 0

    init {
        // Add some sample items.
        for (i in 1..COUNT) {
            addItem(createDummyItem(i))
        }
    }

    fun addItem(item: Comida) {
        ITEMS.add(item)
        ITEM_MAP.put(item.id, item)
    }

    fun updateItem(item: Comida) {
        ITEMS.set(ITEMS.indexOf(item.id), item)
        ITEM_MAP.put(item.id, item)
    }

    fun deleteItem(item: Comida) {
        ITEMS.remove(item)
        ITEM_MAP.remove(item)
    }

    private fun createDummyItem(position: Int): Comida {
        return Comida(position.toString(), "Item " + position, makeDetails(position))
    }

    private fun makeDetails(position: Int): String {
        val builder = StringBuilder()
        builder.append("Details about Item: ").append(position)
        for (i in 0..position - 1) {
            builder.append("\nMore details information here.")
        }
        return builder.toString()
    }

    /**
     * A dummy item representing a piece of content.
     */
    data class Comida(var id: String, var nombre: String, var precio: String) {
        constructor() : this("", "", "") {}
        constructor(nombre: String) : this("", nombre, "") {}
        constructor(nombre: String, precio: String): this("", nombre, precio)

        //override fun toString(): String = nombre
    }
}