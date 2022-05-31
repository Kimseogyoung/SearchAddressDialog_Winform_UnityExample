using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA_Dialog
{

    public partial class Form1 : Form
    {
        protected delegate void ExitHandler(Locale lc);//play- 바로 재생할것인지
        protected event ExitHandler OnExit;

        Locale current = null;
        FormManager fm;
        List<Locale> localeList;
        bool isAddressSearch = true;
        public Form1()
        {
            localeList = new List<Locale>();

            fm = new FormManager();

            InitializeComponent();

            //InitializeChromeBrowser();
           
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnExit?.Invoke(current) ;
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(isAddressSearch)
                localeList = fm.AddressSearchRequest(this.textBox1.Text);
            else
                localeList = fm.kewordSearchRequest(this.textBox1.Text);

            this.listBox1.Items.Clear();

            foreach(Locale lc in localeList)
            {
                this.listBox1.Items.Add(lc.name);
            }

        }
        private void listBox1_DBClick(object sender, MouseEventArgs e)
        {
            // 인덱스를 저장할 변수
            int selectedIndex = -1;

            // 마우스 포인터의 위치
            Point point = e.Location;

            // 리스트 박스의 IndexFromPoint 메서드 호출
            selectedIndex = this.listBox1.IndexFromPoint(point);

            if (selectedIndex != -1) // 빈 공간이 아닌 곳을 더블클릭 했을 때.
            {
                // 선택된 항목 저장
                string selectedItem = this.listBox1.Items[selectedIndex] as string;
                current = localeList[selectedIndex];
             }

        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ChangedSearchType(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;

            string msg = string.Empty;



            if (btn.Checked == false) //라디오 버튼 컨트롤 체크 안되어 있으면

                return;



            if (btn.Text == "주소 검색(도로명, 지명)") isAddressSearch = true;
            else isAddressSearch = false;

        }
    }
}
