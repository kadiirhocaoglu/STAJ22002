import { Col, Row, Statistic } from 'antd';
import CountUp from 'react-countup';
const formatter = (value) => <CountUp end={value} separator="," />;

const ChatDashboardPage = () => {
  return (
    
    <div><Row gutter={16}>
    <Col span={12}>
      <Statistic title="Destek Bekleyen Kullanıcı" value={1304} formatter={formatter} />
    </Col>
    <Col span={12}>
      <Statistic title="Çözülmüş Sorunlar" value={1128} precision={2} formatter={formatter} />
    </Col>
  </Row></div>
);
}

export default ChatDashboardPage