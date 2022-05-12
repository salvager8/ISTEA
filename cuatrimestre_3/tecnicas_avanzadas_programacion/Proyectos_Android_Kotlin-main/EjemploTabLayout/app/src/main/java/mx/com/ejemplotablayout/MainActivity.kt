package mx.com.ejemplotablayout

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.viewpager.widget.ViewPager
import com.google.android.material.tabs.TabLayout
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    var viewPager:ViewPager? = null
    var tabLayout:TabLayout?= null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        viewPager = findViewById(R.id.viewPager)
        tabLayout = findViewById(R.id.tabsLayout)

        configurarViewPager()
        tabLayout?.setupWithViewPager(viewPager)

        configurarIconos()
    }

    fun configurarViewPager() {
        val adapter = ViewPagerAdapter(supportFragmentManager)
        adapter.addFragment(Fragment01(), "Fragment01")
        adapter.addFragment(Fragment01(), "Fragment02")
        adapter.addFragment(Fragment01(), "Fragment03")

        viewPager?.adapter = adapter
    }

    fun configurarIconos() {
        tabLayout?.getTabAt(0)!!.setIcon(R.drawable.ic_camera)
        tabLayout?.getTabAt(1)!!.setIcon(R.drawable.ic_call)
        tabLayout?.getTabAt(2)!!.setIcon(R.drawable.ic_shopping_cart)
    }

}