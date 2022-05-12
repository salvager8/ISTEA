package mx.com.ejemplotablayout

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentPagerAdapter

class ViewPagerAdapter(fragmentManager: FragmentManager):FragmentPagerAdapter(fragmentManager) {

    var fragmentList:ArrayList<Fragment>? = null
    var fragmentTitleList:ArrayList<String>? = null

    init {
        fragmentList = ArrayList()
        fragmentTitleList = ArrayList()
    }

    override fun getCount(): Int {
       return fragmentList?.size!!
    }

    override fun getItem(position: Int): Fragment {
        return fragmentList?.get(position)!!
    }

    fun addFragment(fragment: Fragment, title: String) {
        fragmentList?.add(fragment)
        fragmentTitleList?.add(title)
    }

    override fun getPageTitle(position: Int): String {
        return fragmentTitleList?.get(position)!!
    }

}