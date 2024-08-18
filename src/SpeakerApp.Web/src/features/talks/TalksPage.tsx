import React from 'react';
import Layout from '../layout/Layout';
import TalksList from './TalksList';

const TalksPage: React.FC = () => {
return (
    <Layout>
        <div>
            <h2 className='float-start'>Talks</h2>
            <a href='/talks/create' className="btn btn-primary float-end" style={{ float: 'right' }} >Create Talk</a>
        </div>
        <div className="clearfix"></div>
        <div>
            <TalksList showSpeaker={true}/>
        </div>
    </Layout>
);
};

export default TalksPage;
